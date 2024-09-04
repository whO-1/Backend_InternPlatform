using internPlatform.Domain.Models.ApiModels.StatisticsModels;
using internPlatform.Domain.Models.ViewModels.StatisticsViewModels;
using System.Collections.Generic;

namespace internPlatform.Application.Services.Statistics
{
    public interface IStatisticsService
    {

        List<StatisticCard> CountUsersEvents();
        List<StatisticCard> CountUsersInfo();
        StatisticBar CountLastHalfYearPosts();
        StatisticBar CountPostsPerCategory();
        List<StatisticCard> CountPostsInfo(string userId);
        List<RecentPosted> CountRecentPosted(string userId);
        List<TopPost> CountTopPosts(string userId);
        StatisiticInteractions CountLastHalfYearPostsUser(string userId);
    }
}
