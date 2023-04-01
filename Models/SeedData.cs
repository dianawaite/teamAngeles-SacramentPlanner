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
            if (context.Hymn.Any() == false)
            {
                //return;   // DB has been seeded
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
            }


            // MEMBER
            if (context.Member.Any() == false)
            {
                // return;   // DB has been seeded
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
            }


            // TOPICS
            if (context.Topic.Any() == false)
            {
                //return;   // DB has been seeded
                context.Topic.AddRange(
                    new Topic
                    {
                        Name = "Charity",
                        Quote = "Wherefore, cleave unto charity, which is the greatest of all, for all things must fail—but charity is the pure love of Christ, and it endureth forever (Moroni 7:46–47)"
                    },
                    new Topic
                    {
                        Name = "Hope",
                        Quote = "Regardless of how desparate things may seem, remember-we can always have hope. Always! ~President Russel M. Nelson"
                    },
                    new Topic
                    {
                        Name = "Faith",
                        Quote = "Faith in the Savior taught me that no matter what happened in the past, my story could have a happy ending. ~President Dieter F. Uchtdorf"
                    },
                    new Topic
                    {
                        Name = "Forgiveness",
                        Quote = "Ye ought to forgive one another; for he that forgiveth not his brother his trespasses standeth condemned before the Lord; for there remaineth in him the greater sin. I, the Lord, will forgive whom I will forgive, but of you it is required to forgive all men (Doctrine and Covenants 64:9–10)"
                    },
                    new Topic
                    {
                        Name = "Miracles",
                        Quote = "There are miracles all around, if we have eyes to see. ~Elder Ronald A. Rasband"
                    }
                );
                context.SaveChanges();
            }


            // MEETING
            if (context.Meeting.Any() == false)
            {
                // return;   // DB has been seeded

                Meeting meeting = new Meeting
                {
                    Congregation = "BYU-I Ward",
                    MeetingDate = DateTime.Parse("2023-4-16"),
                    Conducting = "Henry B. Eyring",
                    OpeningPrayer = "Kristen Glenn",
                    ClosingPrayer = "Jason Williams",
                    OpeningHymn = "The Morning Breaks",
                    SacramentHymn = "As Now We Take the Sacrament",
                    ClosingHymn = "Secret Prayer",
                    Topic = "Hope"
                };

                context.Meeting.AddRange(
                    meeting
                );
                context.SaveChanges();
                int meeting_id = meeting.Id;

                // add meeting speakers
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
}
