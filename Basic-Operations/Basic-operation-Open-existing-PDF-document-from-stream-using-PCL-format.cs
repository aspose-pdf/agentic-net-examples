using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PCL file
        const string pclPath = "input.pcl";
        // Path where the resulting PDF will be saved
        const string outputPdf = "output.pdf";

        // Verify that the PCL file exists before attempting to load it
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        try
        {
            // Open the PCL file as a stream
            using (FileStream pclStream = new FileStream(pclPath, FileMode.Open, FileAccess.Read))
            {
                // Create load options for PCL conversion
                PclLoadOptions loadOptions = new PclLoadOptions();

                // Load the PCL stream into a PDF Document using the load options
                Document pdfDocument = new Document(pclStream, loadOptions);

                // Save the resulting PDF document to the specified output path
                pdfDocument.Save(outputPdf);
            }

            Console.WriteLine($"PDF successfully created at: {outputPdf}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}