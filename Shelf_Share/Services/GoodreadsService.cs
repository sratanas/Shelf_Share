using Shelf_Share.Data;
using Shelf_Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelf_Share.Services
{
    public class GoodreadsService : IGoodreadsService
    {
        private readonly IGoodreadsRepository _goodreadsRepository;

        public GoodreadsService(IGoodreadsRepository goodreadsRepository)
        {
            _goodreadsRepository = goodreadsRepository;
        }

        public async Task<GoodreadsResponse> GetBookBasedOnTitleInput(string searchInput)
        {
            return await _goodreadsRepository.ReturnBookBasedOnTitle(searchInput);
        }
    }
}
