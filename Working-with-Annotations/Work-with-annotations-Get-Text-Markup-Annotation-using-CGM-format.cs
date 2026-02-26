using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // CGM is input‑only; load it with CgmLoadOptions and treat it as a PDF document
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Optional: save the converted PDF for inspection
            doc.Save(outputPdf);

            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only text markup annotations (highlight, underline, etc.)
                    if (annot is TextMarkupAnnotation markup)
                    {
                        // Retrieve the marked text as a plain string
                        string markedText = markup.GetMarkedText();
                        Console.WriteLine($"Page {page.Number}, Annotation {markup.AnnotationType}:");
                        Console.WriteLine($"Marked text: {markedText}");

                        // Retrieve the marked text as fragments (useful for detailed analysis)
                        TextFragmentCollection fragments = markup.GetMarkedTextFragments();
                        foreach (TextFragment fragment in fragments)
                        {
                            Console.WriteLine($"  Fragment: \"{fragment.Text}\"");
                        }
                    }
                }
            }
        }
    }
}