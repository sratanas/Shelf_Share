using System.Threading.Tasks;
using Shelf_Share.Models;

namespace Shelf_Share.Data
{
    public interface IGoodreadsRepository
    {
        Task<GoodreadsResponse> ReturnBookBasedOnTitle(string searchInput);
    }
}