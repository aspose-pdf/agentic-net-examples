using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int expectedFieldCount = 5; // adjust to the expected number of form fields

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Verify the number of form fields
                int actualCount = doc.Form.Count;
                Console.WriteLine($"Form fields found: {actualCount}");

                if (actualCount != expectedFieldCount)
                {
                    Console.Error.WriteLine($"Unexpected number of form fields. Expected {expectedFieldCount}, but found {actualCount}.");
                    return;
                }

                // Proceed with further processing now that the count is verified
                Console.WriteLine("Form field count verification passed.");
                // Example processing could be added here
                // doc.Save("processed.pdf"); // Save if needed
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}