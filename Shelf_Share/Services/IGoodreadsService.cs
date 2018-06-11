using System.Threading.Tasks;
using Shelf_Share.Models;

namespace Shelf_Share.Services
{
    public interface IGoodreadsService
    {
        Task<GoodreadsResponse> GetBookBasedOnTitleInput(string searchInput);
    }
}