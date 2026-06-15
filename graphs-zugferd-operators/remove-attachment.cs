using System;
using System.IO;
using Aspose.Pdf;

class RemoveAttachmentExample
{
    static void Main()
    {
        // Create a sample PDF with two embedded files
        string inputPath = "input.pdf";
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // First attachment
            using (MemoryStream ms1 = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms1))
                {
                    writer.Write("This is the content of file1.");
                    writer.Flush();
                    ms1.Position = 0;
                    FileSpecification fileSpec1 = new FileSpecification("file1.txt", "file1.txt");
                    fileSpec1.Contents = ms1;
                    doc.EmbeddedFiles.Add(fileSpec1);
                }
            }

            // Second attachment
            using (MemoryStream ms2 = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms2))
                {
                    writer.Write("This is the content of file2.");
                    writer.Flush();
                    ms2.Position = 0;
                    FileSpecification fileSpec2 = new FileSpecification("file2.txt", "file2.txt");
                    fileSpec2.Contents = ms2;
                    doc.EmbeddedFiles.Add(fileSpec2);
                }
            }

            doc.Save(inputPath);
        }

        // Open the PDF and delete a specific attachment
        string outputPath = "output.pdf";
        using (Document doc = new Document(inputPath))
        {
            // Remove the attachment named "file1.txt"
            doc.EmbeddedFiles.Delete("file1.txt");
            doc.Save(outputPath);
        }

        Console.WriteLine("Attachment 'file1.txt' removed. Result saved as " + outputPath);
    }
}
