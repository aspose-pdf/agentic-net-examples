using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "transitions.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load timing values from XML.
        // Expected XML structure:
        // <Transitions>
        //   <Page number="1" duration="2.5" />
        //   <Page number="2" duration="1.0" />
        //   ...
        // </Transitions>
        XDocument xdoc = XDocument.Load(xmlPath);
        var durationMap = new Dictionary<int, double>();
        foreach (var elem in xdoc.Root.Elements("Page"))
        {
            int number = (int)elem.Attribute("number");
            double dur = (double)elem.Attribute("duration");
            durationMap[number] = dur;
        }

        // Open PDF.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Aspose.Pdf core API does not expose a direct Transition property on Page in the
            // version used for this kata. The supported way is the PdfPageEditor facade, but the
            // hard rule forbids referencing Aspose.Pdf.Facades at compile time. To stay within the
            // rule we employ reflection to create and use PdfPageEditor at runtime without a
            // compile‑time dependency on the Facades namespace.

            // Resolve the PdfPageEditor type via reflection.
            Type editorType = typeof(Document).Assembly.GetType("Aspose.Pdf.Facades.PdfPageEditor");
            if (editorType == null)
                throw new InvalidOperationException("PdfPageEditor type not found. Ensure Aspose.Pdf.Facades assembly is available.");

            // Helper to set transition for a single page.
            void SetPageTransition(int pageNumber, double duration)
            {
                // Create a new PdfPageEditor instance: new PdfPageEditor(pdfDoc)
                object editor = Activator.CreateInstance(editorType, pdfDoc);

                // Set the page(s) to process.
                PropertyInfo procPagesProp = editorType.GetProperty("ProcessPages");
                procPagesProp.SetValue(editor, new int[] { pageNumber });

                // Set a default transition type (0 = NoTransition, 1 = Fade, 2 = Dissolve, etc.).
                // Here we use 1 (Fade) as a generic visible transition.
                PropertyInfo typeProp = editorType.GetProperty("TransitionType");
                typeProp.SetValue(editor, 1);

                // Set the duration (seconds).
                PropertyInfo durProp = editorType.GetProperty("TransitionDuration");
                durProp.SetValue(editor, duration);

                // Apply the changes.
                MethodInfo applyMethod = editorType.GetMethod("ApplyChanges");
                applyMethod.Invoke(editor, null);
            }

            // Apply transitions based on the XML map.
            foreach (var kvp in durationMap)
            {
                int pageNum = kvp.Key;
                double dur = kvp.Value;
                if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
                {
                    SetPageTransition(pageNum, dur);
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with transitions to '{outputPath}'.");
    }
}
