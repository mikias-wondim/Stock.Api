namespace Stock.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock item);
        Task<Stock> UpdateAsync(int id, Stock item);
        Task<Stock> DeleteAsync(int id);
    }
}