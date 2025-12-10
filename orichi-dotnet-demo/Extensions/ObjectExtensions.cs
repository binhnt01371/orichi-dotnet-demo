using System.Reflection;

namespace orichi_dotnet_demo.Extensions
{
    public static class ObjectExtensions
    {
        public static int AverageNumericProperties(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            int totalSum = 0;
            int numericPropertyCount = 0;
            Type type = obj.GetType();

            // Get all public instance properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                // Check if the property type is numeric (int, double, float, decimal, etc.)
                if (IsNumericType(prop.PropertyType))
                {
                    // Get the value of the property for the given object
                    object value = prop.GetValue(obj);
                    if (value != null)
                    {
                        // Convert the value to a int for the average calculation
                        totalSum += Convert.ToInt32(value);
                        numericPropertyCount++;
                    }
                }
            }

            if (numericPropertyCount == 0) return 0;
            return totalSum / numericPropertyCount;
        }

        private static bool IsNumericType(Type type)
        {
            // Handle Nullable numeric types
            Type effectiveType = Nullable.GetUnderlyingType(type) ?? type;

            var numericTypes = new HashSet<Type>
            {
                typeof(int), typeof(double), typeof(decimal), typeof(long),
                typeof(short), typeof(float)
            };

            return numericTypes.Contains(effectiveType);
        }
    }
}
