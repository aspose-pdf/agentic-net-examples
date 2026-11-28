using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentsFolder = "Attachments";
        const string reportPath = "AttachmentsReport.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(attachmentsFolder);

        // Prepare the report writer
        using (StreamWriter reportWriter = new StreamWriter(reportPath, false))
        {
            // Load the PDF document – embedded files are accessed via Document.EmbeddedFiles
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                var embeddedFiles = pdfDocument.EmbeddedFiles;
                int attachmentCount = embeddedFiles != null ? embeddedFiles.Count : 0;

                reportWriter.WriteLine($"Total attachments found: {attachmentCount}");
                reportWriter.WriteLine();

                // Iterate over each embedded file and save it to disk
                for (int i = 0; i < attachmentCount; i++)
                {
                    FileSpecification fileSpec = embeddedFiles[i];
                    string attachmentFileName = fileSpec.Name; // original file name
                    string outputFilePath = Path.Combine(attachmentsFolder, attachmentFileName);

                    try
                    {
                        // Ensure the stream is positioned at the beginning
                        if (fileSpec.Contents.CanSeek)
                            fileSpec.Contents.Position = 0;

                        // Write the contents to the output file
                        using (FileStream outStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                        {
                            fileSpec.Contents.CopyTo(outStream);
                        }

                        reportWriter.WriteLine($"{attachmentFileName} - Extracted successfully");
                    }
                    catch (Exception ex)
                    {
                        // If saving fails, log the error
                        reportWriter.WriteLine($"{attachmentFileName} - Extraction failed: {ex.Message}");
                    }
                }
            }
        }

        Console.WriteLine($"Attachment extraction completed. Report saved to '{reportPath}'.");
    }
}
