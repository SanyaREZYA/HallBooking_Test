public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
        => await _repository.GetCustomerByIdAsync(id);

    public async Task<List<Customer>> GetAllCustomersAsync()
        => await _repository.GetAllCustomersAsync();

    public async Task<Customer> CreateCustomerAsync(CustomerDto dto)
    {
        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        return await _repository.CreateCustomerAsync(customer);
    }

    public async Task<Customer?> UpdateCustomerAsync(int id, CustomerDto dto)
    {
        var existingCustomer = await _repository.GetCustomerByIdAsync(id);

        if (existingCustomer is null)
        {
            return null;
        }

        existingCustomer.FirstName = dto.FirstName;
        existingCustomer.LastName = dto.LastName;
        existingCustomer.PhoneNumber = dto.PhoneNumber;
        existingCustomer.Email = dto.Email;

        return await _repository.UpdateCustomerAsync(existingCustomer);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
        => await _repository.DeleteCustomerAsync(id);
}