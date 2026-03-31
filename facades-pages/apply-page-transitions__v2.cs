using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                int pageCount = doc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    int mod = (pageIndex - 1) % 3;
                    if (mod == 0)
                    {
                        editor.TransitionType = PdfPageEditor.BLINDH;
                    }
                    else if (mod == 1)
                    {
                        editor.TransitionType = PdfPageEditor.LRWIPE;
                    }
                    else
                    {
                        editor.TransitionType = PdfPageEditor.DISSOLVE;
                    }

                    editor.ProcessPages = new int[] { pageIndex };
                    editor.ApplyChanges();
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine("Transitions applied and saved to '" + outputPath + "'.");
    }
}
