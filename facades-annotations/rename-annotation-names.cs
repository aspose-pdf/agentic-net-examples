using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed_annotations.pdf";
        const string prefix = "STD_";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    string originalName = annotation.Name;
                    if (!string.IsNullOrEmpty(originalName))
                    {
                        annotation.Name = prefix + originalName;
                    }
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations renamed and saved to '{outputPath}'.");
    }
}