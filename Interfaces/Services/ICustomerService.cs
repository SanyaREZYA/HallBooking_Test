public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(CustomerDto dto);
    Task<Customer?> UpdateCustomerAsync(int id, CustomerDto dto);
    Task<bool> DeleteCustomerAsync(int id);
}