using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logFile  = "extraction_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Log start timestamp
            string startTime = DateTime.Now.ToString("o");
            File.AppendAllText(logFile, $"Extraction started: {startTime}{Environment.NewLine}");

            // Iterate over all pages and extract text from each form (XForm)
            foreach (Page page in doc.Pages)
            {
                foreach (XForm form in page.Resources.Forms)
                {
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(form); // extract text from the form

                    // Write extracted text to the log (optional)
                    File.AppendAllText(logFile,
                        $"Page {page.Number}, Form {form.Name}:{Environment.NewLine}{absorber.Text}{Environment.NewLine}");
                }
            }

            // Log end timestamp
            string endTime = DateTime.Now.ToString("o");
            File.AppendAllText(logFile, $"Extraction finished: {endTime}{Environment.NewLine}");
        }

        Console.WriteLine("Form extraction completed. Timestamps logged.");
    }
}