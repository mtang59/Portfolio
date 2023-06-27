namespace FenceApp
{
    public class SketchData
    {
        // Properties for the sketch data
        // For example: public int Id { get; set; }
    }

    public class LengthData
    {
        public int Length { get; set; }
    }

    public class CustomerData
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }

        public CustomerData()
        {
            CustomerName = string.Empty;
            CustomerAddress = string.Empty;
            CustomerPhoneNumber = 0;
            CustomerEmail = string.Empty;
        }
    }

    public class MaterialData
    {
        // Properties for the material data
        public int Id { get; set; }
    }
}
