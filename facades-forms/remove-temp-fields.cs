using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access the Form object directly from the Document – no constructor needed.
                var form = doc.Form;

                if (form == null)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                }
                else
                {
                    // Retrieve field names via the Fields collection (the correct API).
                    var fieldNames = form.Fields?.Select(f => f.Name).ToArray() ?? Array.Empty<string>();

                    foreach (var fieldName in fieldNames)
                    {
                        if (!string.IsNullOrEmpty(fieldName) && fieldName.StartsWith("Temp_"))
                        {
                            // Delete the temporary field.
                            form.Delete(fieldName);
                            Console.WriteLine($"Deleted field: {fieldName}");
                        }
                    }
                }

                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
