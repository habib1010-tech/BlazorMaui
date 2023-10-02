using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MauiApp1.Data;

    internal class UserService
{
    string _dbPath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection conn;
    public UserService(string dbPath)
    {
        _dbPath = dbPath;
    }
    private async Task InitAsync()
    {
        // Don't Create database if it exists
        if (conn != null)
            return;
        // Create database and UserModel Table
        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<UserModel>();
    }
    public async Task<List<UserModel>> GetUserAsync()
    {
        await InitAsync();
        return await conn.Table<UserModel>().ToListAsync();
    }
    public async Task<UserModel> CreateUserAsync(
        UserModel paramUser)
    {
        // Insert
        await conn.InsertAsync(paramUser);
        // return the object with the
        // auto incremented Id populated
        return paramUser;
    }
    public async Task<UserModel> UpdateUserAsync(
        UserModel paramUser)
    {
        // Update
        await conn.UpdateAsync(paramUser);
        // Return the updated object
        return paramUser;
    }
    public async Task<UserModel> DeleteUserAsync(
        UserModel paramUser)
    {
        // Delete
        await conn.DeleteAsync(paramUser);
        return paramUser;
    }
}

