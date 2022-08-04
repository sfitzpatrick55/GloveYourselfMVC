using System;
using GloveYourself.Data.Data;
using GloveYourself.Data.Models;
using GloveYourself.Models.Glove;

namespace GloveYourself.Services.Glove
{
    public class GloveService : IGloveService
    {
        private readonly Guid _userId;

        private readonly ApplicationDbContext _context;

        public GloveService(Guid userId, ApplicationDbContext context)
        {
            _userId = userId;
            _context = context;
        }

        public bool CreateGlove(GloveCreate model)
        {
            var entity = new GloveEntity()
            {
                OwnerId = _userId,
                Title = model.Title,
                Brand = model.Brand,
                Description = model.Description,
                CreatedUtc = DateTimeOffset.Now
            };

            _context.Gloves.Add(entity);

            return _context.SaveChanges() == 1;
        }

        public IEnumerable<GloveListItem> GetGloves()
        {
            var query = _context.Gloves.Where(g => g.OwnerId == _userId)
                .Select(g => new GloveListItem
                {
                    GloveId = g.GloveId,
                    Title = g.Title,
                    CreatedUtc = g.CreatedUtc
                }
            );

            return query.ToArray();
        }
    }
}

