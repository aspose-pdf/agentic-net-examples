using System;
using System.IO;
using Aspose.Pdf; // Document, FileSpecification, EmbeddedFiles

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentsFolder = "Attachments";
        const string indexFileName = "AttachmentsIndex.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(attachmentsFolder);
        string indexFilePath = Path.Combine(attachmentsFolder, indexFileName);

        try
        {
            // Load the PDF document – this gives access to the EmbeddedFiles collection
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                var embeddedFiles = pdfDocument.EmbeddedFiles;
                int fileCount = embeddedFiles?.Count ?? 0;

                // Create/overwrite the index file
                using (StreamWriter indexWriter = new StreamWriter(indexFilePath, false))
                {
                    for (int i = 0; i < fileCount; i++)
                    {
                        // Each item is a FileSpecification object representing an embedded file
                        FileSpecification fileSpec = embeddedFiles[i];
                        string fileName = fileSpec.Name;

                        // Write the name to the index file
                        indexWriter.WriteLine(fileName);

                        // Save the embedded file to the output folder
                        string outputPath = Path.Combine(attachmentsFolder, fileName);
                        using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            // Ensure the content stream is positioned at the beginning
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(outStream);
                        }
                    }
                }
            }

            Console.WriteLine($"Attachments extracted to '{attachmentsFolder}'.");
            Console.WriteLine($"Index file created at '{indexFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
