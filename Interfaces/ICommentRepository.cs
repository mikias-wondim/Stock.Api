using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Api.Models;

namespace Stock.Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}