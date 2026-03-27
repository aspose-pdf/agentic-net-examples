using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("Usage: AnnotationSummaryReport <pdf-file1> [<pdf-file2> ...]");
            return;
        }

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            ProcessPdf(pdfPath);
        }
    }

    private static void ProcessPdf(string pdfPath)
    {
        using (Document doc = new Document(pdfPath))
        {
            Dictionary<AnnotationType, int> counts = new Dictionary<AnnotationType, int>();

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annotations = page.Annotations;

                for (int j = 1; j <= annotations.Count; j++)
                {
                    Annotation ann = annotations[j];
                    AnnotationType type = ann.AnnotationType;
                    if (counts.ContainsKey(type))
                    {
                        counts[type] = counts[type] + 1;
                    }
                    else
                    {
                        counts.Add(type, 1);
                    }
                }
            }

            Console.WriteLine($"File: {Path.GetFileName(pdfPath)}");
            if (counts.Count == 0)
            {
                Console.WriteLine("  No annotations found.");
            }
            else
            {
                foreach (KeyValuePair<AnnotationType, int> kvp in counts)
                {
                    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                }
            }
        }
    }
}