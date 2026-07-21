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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Capture start timestamp
                DateTime startTime = DateTime.UtcNow;
                Console.WriteLine($"Form extraction started at {startTime:O}");

                // Iterate over pages and extract text from each form XObject
                foreach (Page page in doc.Pages)
                {
                    // Page.Resources.Forms is a collection of XForm objects.
                    // Each XForm may have a Name property; if not, we fall back to an index.
                    int formIndex = 0;
                    foreach (XForm form in page.Resources.Forms)
                    {
                        TextAbsorber absorber = new TextAbsorber();
                        absorber.Visit(form); // extract text from the form

                        string formName = !string.IsNullOrEmpty(form.Name) ? form.Name : $"Form_{formIndex}";
                        Console.WriteLine($"Form '{formName}' on page {page.Number}:");
                        Console.WriteLine(absorber.Text);
                        formIndex++;
                    }
                }

                // Capture end timestamp
                DateTime endTime = DateTime.UtcNow;
                Console.WriteLine($"Form extraction ended at {endTime:O}");
                Console.WriteLine($"Duration: {(endTime - startTime).TotalSeconds} seconds");

                // Save the (potentially modified) document
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
