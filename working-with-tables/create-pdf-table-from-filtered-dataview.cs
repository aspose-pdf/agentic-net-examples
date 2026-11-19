using System;
using System.Data;

class Program
{
    static void Main()
    {
        // Create a source DataTable with sample data
        DataTable source = new DataTable("Source");
        source.Columns.Add("Id", typeof(int));
        source.Columns.Add("Name", typeof(string));
        source.Columns.Add("Amount", typeof(decimal));

        source.Rows.Add(1, "Alice", 120.5m);
        source.Rows.Add(2, "Bob", 80.0m);
        source.Rows.Add(3, "Charlie", 150.0m);
        source.Rows.Add(4, "Diana", 60.0m);

        // Build a DataView on the source table
        DataView view = new DataView(source);

        // Filter rows: keep only records where Amount > 100
        view.RowFilter = "Amount > 100";

        // Import the filtered rows into a new DataTable
        DataTable filtered = view.ToTable();

        // Output the filtered rows to verify the result
        Console.WriteLine("Filtered rows:");
        foreach (DataRow row in filtered.Rows)
        {
            Console.WriteLine($"Id={row["Id"]}, Name={row["Name"]}, Amount={row["Amount"]}");
        }
    }
}