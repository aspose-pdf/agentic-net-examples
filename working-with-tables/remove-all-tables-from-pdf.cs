using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Locate all tables in the document
                TableAbsorber tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(doc);

                // Remove each detected table using the absorber's Remove method
                foreach (var table in tableAbsorber.TableList)
                {
                    tableAbsorber.Remove(table);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All tables removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
