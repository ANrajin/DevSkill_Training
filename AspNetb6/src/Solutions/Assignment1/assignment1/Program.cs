using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Course course = new Course()
            {
                Title = "Asp.Net",
                Teacher = new Instructor()
                {
                    Name = "Jalal Uddin",
                    Email = "jalaluddin@devskill.com",
                    PresentAddress = new Address
                    {
                        Street = "100/A Mirpur",
                        City = "Dhaka",
                        Country = "Bangladesh"
                    },
                    PermanentAddress = new Address
                    {
                        Street = "100/A Mirpur",
                        City = "Dhaka",
                        Country = "Bangladesh"
                    },
                    PhoneNumbers = new List<Phone>
                    {
                        new Phone{Number = "123456789", Extension = "02", CountryCode = "+880"},
                        new Phone{Number = "987456321", Extension = "02", CountryCode = "+880"}
                    }
                },
                Topics = new List<Topic>
                {
                    new Topic
                    {
                        Title = "Linq",
                        Description = "Introduction to linq",
                        Sessions = new List<Session>
                        {
                            new Session
                            {
                                DurationInHour = 4,
                                LearningObjective = "how to query with linq"
                            },
                            new Session
                            {
                                DurationInHour = 4,
                                LearningObjective = "Advance linq query"
                            }
                        }
                    },
                    new Topic
                    {
                        Title = "Delegates & Events",
                        Description = "What is delegate",
                        Sessions = new List<Session>
                        {
                            new Session
                            {
                                DurationInHour = 4,
                                LearningObjective = "How to work with delegates and its usage"
                            },
                            new Session
                            {
                                DurationInHour = 4,
                                LearningObjective = "How to work with events and delegates"
                            }
                        }
                    }
                },
                Fees = 30000,
                Tests = new List<AdmissionTest>
                {
                    new AdmissionTest
                    {
                        StartDateTime = new DateTime(2021, 10, 05),
                        EndDateTime = new DateTime(2021, 10, 10),
                        TestFees = 100
                    },
                    new AdmissionTest
                    {
                        StartDateTime = new DateTime(2021, 09, 24),
                        EndDateTime = new DateTime(2021, 09, 30),
                        TestFees = 100
                    }
                }
            };

            string json = JsonFormatter.Convert<Course>(course);
            Console.WriteLine(json);
        }
    }
}
