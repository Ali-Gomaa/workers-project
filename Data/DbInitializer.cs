using System.Collections.Generic;
using Workers.Models;

namespace Workers.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // لو فيه بيانات بالفعل.. ما تعملش حاجة
            if (context.Projects.Any())
                return;

            var p1 = new Project { Name = "مشروع 1" };
            var p2 = new Project { Name = "مشروع 2" };
            var p3 = new Project { Name = "مشروع 3" };

            context.Projects.AddRange(p1, p2, p3);

            var people = new List<Person>
            {
                new Person { Name = "مدير 1", IsManager = true, Project = p1 },
                new Person { Name = "عامل 1", IsManager = false, Project = p1 },
                new Person { Name = "عامل 2", IsManager = false, Project = p1 },
                new Person { Name = "مدير 2", IsManager = true, Project = p2 },
                new Person { Name = "عامل 3", IsManager = false, Project = p2 },
                new Person { Name = "مدير 3", IsManager = true, Project = p3 },
                new Person { Name = "عامل 4", IsManager = false, Project = p3 },
            };

            context.People.AddRange(people);
            context.SaveChanges();
        }

    }
}
