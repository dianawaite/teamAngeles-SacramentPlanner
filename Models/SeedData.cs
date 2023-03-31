using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SacramentPlanner.Data;
using System;
using System.Linq;

namespace SacramentPlanner.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SacramentPlannerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SacramentPlannerContext>>()))
        {

            // HYMN
            if (context.Hymn.Any())
            {
                return;   // DB has been seeded
            }
            context.Hymn.AddRange(
                new Hymn
                {
                    Title = "The Morning Breaks",
                    Page = "1",
                    Sacrament = false
                },
                new Hymn
                {
                    Title = "Secret Prayer",
                    Page = "144",
                    Sacrament = false
                },
                new Hymn
                {
                    Title = "As Now We Take the Sacrament",
                    Page = "169",
                    Sacrament = true
                },
                new Hymn
                {
                    Title = "While of These Emblems We Partake",
                    Page = "173",
                    Sacrament = true
                },
                new Hymn
                {
                    Title = "Away in a Manger",
                    Page = "206",
                    Sacrament = false
                }
            );
            context.SaveChanges();


            // MEMBER
            if (context.Member.Any())
            {
                return;   // DB has been seeded
            }
            context.Member.AddRange(
                new Member
                {
                    Name = "Russel M. Nelson",
                    Bishopric = true
                },
                new Member
                {
                    Name = "Dallin H. Oaks",
                    Bishopric = true
                },
                new Member
                {
                    Name = "Henry B. Eyring",
                    Bishopric = true
                },
                new Member
                {
                    Name = "Josh Allen",
                    Bishopric = false
                },
                new Member
                {
                    Name = "Jason Williams",
                    Bishopric = false
                },
                new Member
                {
                    Name = "Anna Durfee",
                    Bishopric = false
                },
                new Member
                {
                    Name = "Kristen Glenn",
                    Bishopric = false
                },
                new Member
                {
                    Name = "Jacqueline Harris",
                    Bishopric = false
                }
            );
            context.SaveChanges();


            // TOPICS
            if (context.Topic.Any())
            {
                return;   // DB has been seeded
            }
            context.Topic.AddRange(
                new Topic
                {
                    Name = "Charity"
                },
                new Topic
                {
                    Name = "Hope"
                },
                new Topic
                {
                    Name = "Faith"
                },
                new Topic
                {
                    Name = "Forgiveness"
                },
                new Topic
                {
                    Name = "Miracles"
                }
            );
            context.SaveChanges();


            // MEETING
            if (context.Meeting.Any())
            {
                return;   // DB has been seeded
            }

            Meeting meeting = new Meeting
            {
                Congregation = "BYU-I Ward",
                MeetingDate = DateTime.Parse("2023-4-16"),
                Conducting = "Henry B. Eyring",
                OpeningPrayer = "Kristen Glenn",
                ClosingPrayer = "Jason Williams",
                OpeningHymn = "The Morning Breaks",
                SacramentHymn = "As Now We Take the Sacrament",
                IntermediateHymn = "Secret Prayer"
            };

            context.Meeting.AddRange(
                meeting
            );
            context.SaveChanges();
            int meeting_id = meeting.Id;


            // SPEAKER
            if (context.Speaker.Any())
            {
                return;   // DB has been seeded
            }
            context.Speaker.AddRange(
                new Speaker
                {
                    Meeting = meeting_id,
                    Name = "Josh Allen",
                    Subject = "Charity"
                },
                new Speaker
                {
                    Meeting = meeting_id,
                    Name = "Jacqueline Harris",
                    Subject = "Miracles"
                }
            );
            context.SaveChanges();
        }
    }
}
