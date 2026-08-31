public interface ICustomerRepository
{
    Task<List<Customer>> GetAllCustomersAsync();

    Task<Customer?> GetCustomerByIdAsync(int id);

    Task<Customer> CreateCustomerAsync(Customer customer);

    Task<Customer?> UpdateCustomerAsync(Customer customer);

    Task<bool> DeleteCustomerAsync(int id);
}