using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string outputPdf = "output.pdf";

        // Verify the source PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        try
        {
            // Open the PCL file as a stream
            using (FileStream pclStream = File.OpenRead(pclPath))
            {
                // Configure loading options for PCL format
                PclLoadOptions loadOptions = new PclLoadOptions();
                // Example: disable font license verification if needed
                // loadOptions.DisableFontLicenseVerifications = true;

                // Load the PCL stream into a Document instance
                using (Document doc = new Document(pclStream, loadOptions))
                {
                    // Save the resulting PDF document
                    doc.Save(outputPdf);
                }
            }

            Console.WriteLine($"PCL file successfully converted to PDF: '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}