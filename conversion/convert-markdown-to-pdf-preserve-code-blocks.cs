using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the directory of the executing assembly as the data directory.
        // This works both in Visual Studio and when the program is published.
        string dataDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input Markdown file.
        string mdFile = Path.Combine(dataDir, "MD-to-PDF.md");

        // Output PDF file.
        string pdfFile = Path.Combine(dataDir, "MD-to-PDF.pdf");

        // If the markdown file does not exist, create a simple sample file.
        if (!File.Exists(mdFile))
        {
            string sampleMd = "# Sample Markdown\n\nThis is a *sample* markdown file used to demonstrate\nconversion to PDF with Aspose.Pdf.\n\n```csharp\nConsole.WriteLine(\"Hello, Aspose!\");\n```\n";
            File.WriteAllText(mdFile, sampleMd);
        }

        // Initialize load options for Markdown conversion.
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Open the markdown file with a FileStream that allows shared read/write access.
        // This prevents the "file is being used by another process" IOException that can
        // occur when Aspose.Pdf tries to open the file while it is still locked.
        using (FileStream mdStream = new FileStream(mdFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            // Load the Markdown file and convert it to PDF using the stream overload.
            using (Document pdfDocument = new Document(mdStream, mdLoadOptions))
            {
                // Save the resulting PDF. Code blocks are preserved by default.
                pdfDocument.Save(pdfFile);
            }
        }

        Console.WriteLine($"Markdown file has been converted to PDF: {pdfFile}");
    }
}
