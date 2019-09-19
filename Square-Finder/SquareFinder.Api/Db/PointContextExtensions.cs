using SquareFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquareFinder.Api.Db
{
    public static class PointContextExtensions
    {
        public static void EnsureSeedDataForContext(this PointContext context)
        {
            if (context.Points.Any())
            {
                return;
            }

            var points = new List<PointEntity>()
            {
                new PointEntity()
                {
                    X = 1,
                    Y = 1,
                },
                 new PointEntity()
                {
                    X = 2,
                    Y = 2,
                },
                  new PointEntity()
                {
                    X = -1,
                    Y = 1,
                }
            };

            context.Points.AddRange(points);
            context.SaveChanges();
        }
    }
}
