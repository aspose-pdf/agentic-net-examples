using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                foreach (Page page in doc.Pages)
                {
                    // 1. Filter annotations whose author information matches the target.
                    //    In Aspose.Pdf the author is stored in the Subject property of a MarkupAnnotation.
                    var indicesToDelete = new List<int>();

                    for (int i = 1; i <= page.Annotations.Count; i++)
                    {
                        Annotation ann = page.Annotations[i];
                        if (ann is MarkupAnnotation markup &&
                            !string.IsNullOrEmpty(markup.Subject) &&
                            markup.Subject.Equals(targetAuthor, StringComparison.OrdinalIgnoreCase))
                        {
                            indicesToDelete.Add(i);
                        }
                    }

                    // 2. Delete the filtered annotations. Deleting from highest index to lowest
                    //    preserves the validity of the remaining indices.
                    indicesToDelete.Sort((a, b) => b.CompareTo(a));
                    foreach (int idx in indicesToDelete)
                    {
                        page.Annotations.Delete(idx);
                    }
                }

                doc.Save(outputPath);
                Console.WriteLine($"Annotations by '{targetAuthor}' removed. Saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
