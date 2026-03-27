using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationDuplicateChecker
{
    public static void CheckDuplicateAnnotationNames(string inputPath)
    {
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.Ordinal);
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;
                foreach (Annotation annotation in annotations)
                {
                    string name = annotation.Name;
                    if (String.IsNullOrEmpty(name))
                    {
                        continue;
                    }

                    if (nameCounts.ContainsKey(name))
                    {
                        nameCounts[name] = nameCounts[name] + 1;
                    }
                    else
                    {
                        nameCounts[name] = 1;
                    }
                }
            }

            bool duplicatesFound = false;
            foreach (KeyValuePair<string, int> kvp in nameCounts)
            {
                if (kvp.Value > 1)
                {
                    duplicatesFound = true;
                    Console.WriteLine($"Duplicate annotation name \"{kvp.Key}\" found {kvp.Value} times.");
                }
            }

            if (!duplicatesFound)
            {
                Console.WriteLine("No duplicate annotation names were found.");
            }
        }
    }

    public static void Main()
    {
        const string inputPdf = "input.pdf";
        CheckDuplicateAnnotationNames(inputPdf);
    }
}