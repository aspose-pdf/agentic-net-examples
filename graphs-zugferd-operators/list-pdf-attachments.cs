using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF and add an attachment
        using (Document document = new Document())
        {
            document.Pages.Add();
            string attachmentPath = "sample.txt";
            File.WriteAllText(attachmentPath, "This is a sample attachment.");
            FileSpecification fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath));
            document.EmbeddedFiles.Add(fileSpec);
            document.Save("input.pdf");
        }

        // Open the PDF and list all attachments with their sizes
        using (Document document = new Document("input.pdf"))
        {
            Console.WriteLine("Attachments in PDF:");
            EmbeddedFileCollection embeddedFiles = document.EmbeddedFiles;
            for (int i = 1; i <= embeddedFiles.Count; i++)
            {
                FileSpecification spec = embeddedFiles[i];
                string name = spec.Name;
                long sizeInBytes = 0;
                if (spec.Params != null && spec.Params.Size > 0)
                {
                    sizeInBytes = spec.Params.Size;
                }
                else if (spec.Contents != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        spec.Contents.CopyTo(ms);
                        sizeInBytes = ms.Length;
                    }
                }
                Console.WriteLine("- " + name + ": " + sizeInBytes + " bytes");
            }
        }
    }
}