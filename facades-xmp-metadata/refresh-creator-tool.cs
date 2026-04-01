using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string repositoryPath = "pdf-repo";
        string creatorToolName = "MyCreatorTool";

        if (!Directory.Exists(repositoryPath))
        {
            Console.Error.WriteLine($"Repository directory not found: {repositoryPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(repositoryPath, "*.pdf");
        foreach (string pdfFile in pdfFiles)
        {
            try
            {
                using (Document document = new Document(pdfFile))
                {
                    document.Info.Creator = creatorToolName;
                    document.Save(pdfFile);
                }
                Console.WriteLine($"Updated CreatorTool for: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process {Path.GetFileName(pdfFile)}: {ex.Message}");
            }
        }

        Console.WriteLine("CreatorTool refresh completed.");
    }
}