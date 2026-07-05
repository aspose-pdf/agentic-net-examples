using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

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
                // Retrieve the form field named "Address"
                // The Form indexer returns a Field; cast to TextBoxField to access MaxLen (character limit)
                var field = doc.Form["Address"];
                if (field is TextBoxField addressField)
                {
                    int charLimit = addressField.MaxLen; // 0 means no limit set
                    Console.WriteLine($"Character limit for 'Address' field: {charLimit}");
                }
                else
                {
                    Console.WriteLine("The 'Address' field was not found or is not a text box field.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
