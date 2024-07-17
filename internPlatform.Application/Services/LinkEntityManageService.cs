﻿using internPlatform.Application.Services.Mappings;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public class LinkEntityManageService : EntityManageService<Link, LinkDTO>, ILinkEntityManageService
    {
        private readonly IRepository<Link> _repository;
        public LinkEntityManageService(IRepository<Link> repository, LinkConvertor mapper) : base(repository, mapper)
        {
            _repository = repository;

        }
        public List<JTSelectListItem> GetOptions()
        {
            IEnumerable<LinkDTO> results = GetAll();
            var options = new List<JTSelectListItem>();
            options.Add(
                new JTSelectListItem
                {
                    Value = null,
                    DisplayText = "",
                    Selected = true
                }
            );

            if (results.Any())
            {
                foreach (var r in results)
                {
                    options.Add(new JTSelectListItem { Value = r.Id.ToString(), DisplayText = r.LinkTitle });
                }
            }
            return options;
        }

        public async Task<bool> ValidateNewLink(Link updatedLink)
        {
            if (updatedLink.Id == updatedLink.HeadId)
            {
                return false;
            }

            var head = await base.Get(e => e.Id == updatedLink.HeadId);
            if (head != null)
            {
                if (head.HeadId == updatedLink.Id)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
