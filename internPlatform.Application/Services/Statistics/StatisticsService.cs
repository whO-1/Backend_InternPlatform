using internPlatform.Domain.Entities;
using internPlatform.Domain.Models.ApiModels.StatisticsModels;
using internPlatform.Domain.Models.ViewModels.StatisticsViewModels;
using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Repository.IRepository;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace internPlatform.Application.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository<User> _repositorytUser;
        private readonly IRepository<Category> _repositorytCategory;
        private readonly IRepository<Event> _repositorytEvent;
        private readonly ApplicationUserManager _userManager;
        private readonly IOwinContext context;

        public StatisticsService(IRepository<Event> repositorytEvent, IRepository<User> repositorytUser, IOwinContext context, IRepository<Category> repositorytCategory)
        {
            _repositorytCategory = repositorytCategory;
            _repositorytEvent = repositorytEvent;
            _repositorytUser = repositorytUser;
            this.context = context;
            _userManager = context.GetUserManager<ApplicationUserManager>();
        }

        public List<StatisticCard> CountUsersEvents()
        {
            DateTime currentDate = DateTime.Now;
            DateTime lastWeek = currentDate.AddDays(-7);

            int beforeLastWeekEventCounter = _repositorytEvent.GetAll(e => e.IsDeleted != true && e.TimeStamp.CreatedDate <= lastWeek).Count();
            int lastweekEventDeletedCounter = _repositorytEvent.GetAll(e => e.TimeStamp.UpdateDate >= lastWeek && e.IsDeleted != false).Count();
            int lastweekEventAddedCounter = _repositorytEvent.GetAll(e => e.TimeStamp.CreatedDate >= lastWeek).Count();
            int totalEventCounter = beforeLastWeekEventCounter + lastweekEventAddedCounter - lastweekEventDeletedCounter;

            int totalUserCounter = _repositorytUser.GetAll(e => e.IsDeleted != true).Count();
            int beforeLastWeekUserCounter = _repositorytUser.GetAll(e => e.TimeStamp.CreatedDate <= lastWeek).Count();
            int lastWeekUserCounter = totalUserCounter - beforeLastWeekUserCounter;

            int totalViewsCounter = 0;
            int beforeLastWeekViewsCounter = 0;
            int lastweekViewsCounter = totalViewsCounter - beforeLastWeekViewsCounter;

            int totalSubscribersCounter = 0;
            int beforeLastWeekSubscribersCounter = 0;
            int lastweekSubscribersCounter = totalViewsCounter - beforeLastWeekViewsCounter;

            var Events = new StatisticCard
            {
                Total = totalEventCounter,
                Increase = (beforeLastWeekEventCounter > 0) ? Math.Round((double)((lastweekEventAddedCounter - lastweekEventDeletedCounter) * 100) / beforeLastWeekEventCounter, 2) : (totalEventCounter > 0) ? 100 : 0,
                Title = "Events"
            };
            var Users = new StatisticCard
            {
                Total = totalUserCounter,
                Increase = (beforeLastWeekUserCounter != 0) ? (double)(lastWeekUserCounter * 100) / beforeLastWeekUserCounter : (totalUserCounter > 0) ? 100 : 0,
                Title = "Users"
            };

            var Views = new StatisticCard
            {
                Total = totalViewsCounter,
                Increase = (beforeLastWeekViewsCounter != 0) ? (double)(lastweekViewsCounter * 100) / beforeLastWeekViewsCounter : (totalViewsCounter > 0) ? 100 : 0,
                Title = "Views"
            };
            var Subscribers = new StatisticCard
            {
                Total = totalSubscribersCounter,
                Increase = (beforeLastWeekSubscribersCounter != 0) ? (double)(lastweekSubscribersCounter * 100) / beforeLastWeekSubscribersCounter : (totalSubscribersCounter > 0) ? 100 : 0,
                Title = "Subscribers"
            };

            return new List<StatisticCard> { Events, Users, Views, Subscribers };
        }


        public StatisticBar CountLastHalfYearPosts()
        {
            List<string> months = new List<string>();
            List<int> posted = new List<int>();
            List<int> deleted = new List<int>();
            DateTime now = DateTime.Now;

            for (int i = 5; i >= 0; i--)
            {
                DateTime month = now.AddMonths(-i);
                string monthName = month.ToString("MMM yyyy", CultureInfo.InvariantCulture); // Format as "Month Year"
                months.Add(monthName);

                DateTime firstDay = new DateTime(month.Year, month.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

                posted.Add(_repositorytEvent.GetAll(e => (e.TimeStamp.CreatedDate >= firstDay && e.TimeStamp.CreatedDate <= lastDay) && e.IsDeleted != true).Count());
                deleted.Add(_repositorytEvent.GetAll(e => (e.TimeStamp.CreatedDate >= firstDay && e.TimeStamp.CreatedDate <= lastDay) && e.IsDeleted != false).Count());
            }
            return new StatisticBar
            {
                Labels = months,
                Posted = posted,
                Deleted = deleted
            };
        }

        public StatisticBar CountPostsPerCategory()
        {
            List<int> vals = new List<int>();
            List<string> categories = _repositorytCategory.GetAll().Select(e => e.Name).ToList();
            foreach (var category in categories)
            {
                vals.Add(_repositorytEvent.GetAll(e => e.IsDeleted != true && e.Categories.Select(c => c.Name).Contains(category)).Count());
            }
            return new StatisticBar
            {
                Labels = categories,
                Posted = vals
            };
        }

        public List<StatisticCard> CountUsersInfo()
        {

            var Auth = new StatisticCard
            {
                Total = _userManager.Users.Where(u => u.EmailConfirmed != false).Count(),
                Title = "AuthUsers"
            };
            var Active = new StatisticCard
            {
                Total = _repositorytUser.GetAll(u => u.Events.Count() > 0).Count(),
                Title = "ActiveUsers"
            };
            var Passive = new StatisticCard
            {
                Total = _repositorytUser.GetAll(u => u.Events.Count() == 0).Count(),
                Title = "PassiveUsers"
            };
            var Blocked = new StatisticCard
            {
                Total = _userManager.Users.Where(u => u.LockoutEnabled != false && u.LockoutEndDateUtc > DateTime.UtcNow).Count(),
                Title = "BlockedUsers"
            };
            var Total = new StatisticCard
            {
                Total = _repositorytUser.GetAll().Count(),
                Title = "TotalUsers"
            };

            return new List<StatisticCard> { Auth, Active, Passive, Blocked, Total };
        }


        //Admin
        public List<StatisticCard> CountPostsInfo(string userId)
        {
            DateTime today = DateTime.Today;
            int activePosts = _repositorytEvent.GetAll(e => e.IsDeleted != true).Where(e => e.User.UserId == userId).Count();
            int todayPosts = _repositorytEvent.GetAll(e => e.IsDeleted != true).Where(e => e.User.UserId == userId).Where(e => e.StartDate.Date.Equals(today.Date)).Count();
            //to be done
            int totalLikes = 0;
            _repositorytEvent.GetAll().Where(e => e.User.UserId == userId).Select(e => e.Likes).ToList().ForEach(l =>
            {
                totalLikes += l;
            });
            int totalViews = 0;
            _repositorytEvent.GetAll().Where(e => e.User.UserId == userId).Select(e => e.Views).ToList().ForEach(v =>
            {
                totalViews += v;
            });

            //---------------
            int blockedPosts = _repositorytEvent.GetAll(e => e.IsDeleted != true && e.IsBlocked != false).Where(e => e.User.UserId == userId).Count();
            int totalPosts = _repositorytEvent.GetAll().Where(e => e.User.UserId == userId).Count();
            var Active = new StatisticCard
            {
                Total = activePosts,
                Title = "ActivePosts"
            };
            var Today = new StatisticCard
            {
                Total = todayPosts,
                Title = "TodayPosts"
            };
            var TotalLikes = new StatisticCard
            {
                Total = totalLikes,
                Title = "TotalLikes"
            };
            var TotalViews = new StatisticCard
            {
                Total = totalViews,
                Title = "TotalViews"
            };
            var Blocked = new StatisticCard
            {
                Total = blockedPosts,
                Title = "BlockedPosts"
            };
            var Total = new StatisticCard
            {
                Total = totalPosts,
                Title = "TotalPosts"
            };

            return new List<StatisticCard> { Active, Today, TotalLikes, TotalViews, Blocked, Total };
        }

        static string FormatTimeDifference(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 7)
            {
                int weeks = (int)(timeSpan.TotalDays / 7);
                return $"{weeks} week{(weeks > 1 ? "s" : "")}";
            }
            else if (timeSpan.TotalDays >= 1)
            {
                int days = (int)timeSpan.TotalDays;
                return $"{days} day{(days > 1 ? "s" : "")}";
            }
            else if (timeSpan.TotalHours >= 1)
            {
                int hours = (int)timeSpan.TotalHours;
                return $"{hours} hour{(hours > 1 ? "s" : "")}";
            }
            else if (timeSpan.TotalMinutes >= 1)
            {
                int minutes = (int)timeSpan.TotalMinutes;
                return $"{minutes} minute{(minutes > 1 ? "s" : "")}";
            }
            else
            {
                int seconds = (int)timeSpan.TotalSeconds;
                return $"{seconds} second{(seconds > 1 ? "s" : "")}";
            }
        }
        public List<RecentPosted> CountRecentPosted(string userId)
        {
            DateTime now = DateTime.Now;
            List<RecentPosted> recentPostsList = new List<RecentPosted>();
            var postsList = _repositorytEvent.GetAll(e => e.IsDeleted != true).Where(e => e.User.UserId == userId).OrderByDescending(e => e.TimeStamp.CreatedDate).Take(3).ToList();

            postsList.ForEach(p =>
            {
                TimeSpan diff = now - p.TimeStamp.CreatedDate;
                recentPostsList.Add(new RecentPosted
                {
                    StartDate = p.StartDate.Date.ToString("d").Insert(0, "Start date: "),
                    TimeFromCreation = FormatTimeDifference(diff),
                    PostTitle = p.Title,
                    Title = "RecentPosted"
                });
            });



            return recentPostsList;
        }

        public List<TopPost> CountTopPosts(string userId)
        {
            StringBuilder views = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            DateTime today = DateTime.Today;
            List<TopPost> topPostsList = new List<TopPost>();

            var postsList = _repositorytEvent.GetAll(null, "Entry,Age,Categories").Where(e => e.User.UserId == userId).OrderByDescending(e => e.Likes).Take(5).ToList();

            postsList.ForEach(p =>
            {
                views.Clear();
                views.Append(p.Views.ToString());
                views.Append(" views");

                categories.Clear();
                p.Categories.Select(c => c.Name).ToList().ForEach(n =>
                {
                    categories.Append(n);
                });

                topPostsList.Add(new TopPost
                {
                    PostTitle = p.Title,
                    Categories = categories.ToString(),
                    EntryType = (p.Entry != null) ? p.Entry.Name : null,
                    AgeType = (p.Age != null) ? p.Age.Name : null,
                    Views = views.ToString(),
                    Status = (p.IsBlocked) ? "Blocked" : (p.StartDate.Date == today) ? "In Process..." : "Active",
                    Title = "TopPosts",
                    Likes = p.Likes.ToString(),
                });
            });
            return topPostsList;
        }

        public StatisiticInteractions CountLastHalfYearPostsUser(string userId)
        {
            List<string> months = new List<string>();
            List<int> likes = new List<int>();
            List<int> views = new List<int>();
            DateTime now = DateTime.Now;

            for (int i = 5; i >= 0; i--)
            {
                DateTime month = now.AddMonths(-i);
                string monthName = month.ToString("MMM yyyy", CultureInfo.InvariantCulture);
                months.Add(monthName);

                DateTime firstDay = new DateTime(month.Year, month.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
                int monthLikes = 0;
                int monthViews = 0;
                _repositorytEvent.GetAll(e => e.TimeStamp.CreatedDate >= firstDay && e.TimeStamp.CreatedDate <= lastDay).Where(e => e.User.UserId == userId).Select(e => new { e.Likes, e.Views }).ToList().ForEach(el =>
                {
                    monthLikes += el.Likes;
                    monthViews += el.Views;
                });


                likes.Add(monthLikes);
                views.Add(monthViews);
            }
            return new StatisiticInteractions
            {
                Labels = months,
                Likes = likes,
                Views = views
            };
        }
    }
}
