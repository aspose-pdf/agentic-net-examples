using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfOutput = "output.xfdf";
        const string logFile = "xfdf_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Bind the Form facade to the document
                Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form();
                formFacade.BindPdf(doc);

                // Export XFDF to a memory stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    formFacade.ExportXfdf(xfdfStream);
                    xfdfStream.Position = 0; // reset for reading

                    // Write XFDF to the output file
                    using (FileStream fileOut = new FileStream(xfdfOutput, FileMode.Create, FileAccess.Write))
                    {
                        xfdfStream.CopyTo(fileOut);
                    }

                    // Write XFDF content to the diagnostic log
                    xfdfStream.Position = 0;
                    using (StreamReader reader = new StreamReader(xfdfStream))
                    {
                        string xfdfContent = reader.ReadToEnd();
                        File.AppendAllText(logFile,
                            $"--- XFDF Export at {DateTime.Now:u} ---{Environment.NewLine}{xfdfContent}{Environment.NewLine}");
                    }
                }

                // If further PDF modifications are needed, save the document here
                // doc.Save("modified.pdf");
            }

            Console.WriteLine("XFDF exported and logged successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}