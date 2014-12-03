namespace CalendarAPI.Infrastructure.CalendarServiceManager
{
    using CalendarAPI.Infrastructure.CalendarServiceManager.Contracts;
    using CalendarAPI.Infrastructure.FileUpload;
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using System;
    using System.Collections.Generic;

    public class FileParser : IFileParser
    {
        private IFileManager fileManager;

        // poor man's IoC - use only if no dependency container is present
        public FileParser()
            : this(new TextFileManager())
        {
        }

        public FileParser(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public IEnumerable<IBirthDay> ParseBirthDays(string filePath)
        {
            var birthDays = new List<IBirthDay>();
            using (var UCNFile = this.fileManager.GetFile(filePath))
            {
                var fileLine = UCNFile.ReadLine();

                while (fileLine != null)
                {
                    var peopleUnits = fileLine.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var personUnit in peopleUnits)
                    {
                        try
                        {
                            var unitSplit = personUnit.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            var name = unitSplit[0];
                            var ucn = unitSplit[1];

                            var ucnYear = int.Parse(ucn.Substring(0, 2));
                            var ucnMonth = int.Parse(ucn.Substring(2, 2));
                            var ucnDate = int.Parse(ucn.Substring(4, 2));

                            int birthDate = ucnDate;
                            int birthYear = 1900 + ucnYear;
                            int birthMonth = ucnMonth;

                            // born after 2000 logic
                            if (ucnMonth > 40)
                            {
                                birthYear += 100;
                                birthMonth = ucnMonth - 40;
                            }

                            var birthDay = new BirthDay()
                            {
                                Name = name,
                                Date = new DateTime(birthYear, birthMonth, birthDate)
                            };

                            birthDays.Add(birthDay);
                        }
                        catch (FormatException)
                        {
                            // handle format exception
                        }
                        catch (ArgumentException)
                        {
                            // handle argument excepton
                        }
                    }

                    fileLine = UCNFile.ReadLine();
                }

                return birthDays;
            }
        }
    }
}
