using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await GetCustomerByIdAsync(id);
        if (customer is null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return true;
    }
}