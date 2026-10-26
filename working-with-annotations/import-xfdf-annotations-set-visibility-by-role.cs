using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input files
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output_with_annotations.pdf";

        // The role of the current user (could be obtained from your auth system)
        const string currentUserRole = "Manager";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Parse the XFDF file (XFDF is an XML format)
                XDocument xfdfDoc = XDocument.Load(xfdfPath);

                // XFDF structure: <xfdf><annots><annotation ...>...</annotation></annots></xfdf>
                // We'll iterate over each <annotation> element.
                foreach (XElement annotElem in xfdfDoc.Descendants("annotation"))
                {
                    // Basic required attributes (adjust according to your XFDF schema)
                    // Page number (1‑based)
                    int pageNumber = (int?)annotElem.Attribute("page") ?? 1;
                    // Annotation type (e.g., "Text", "Highlight", etc.)
                    string type = (string)annotElem.Attribute("type") ?? "Text";
                    // Optional user role that is allowed to see this annotation
                    string allowedRole = (string)annotElem.Attribute("UserRole") ?? string.Empty;

                    // Determine visibility based on role
                    bool isVisible = string.IsNullOrEmpty(allowedRole) || allowedRole.Equals(currentUserRole, StringComparison.OrdinalIgnoreCase);

                    // Ensure the page exists
                    if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                        continue; // skip invalid page numbers

                    Page page = pdfDoc.Pages[pageNumber];

                    // Common rectangle coordinates (llx, lly, urx, ury) – adjust as needed
                    double llx = (double?)annotElem.Attribute("llx") ?? 100;
                    double lly = (double?)annotElem.Attribute("lly") ?? 500;
                    double urx = (double?)annotElem.Attribute("urx") ?? 200;
                    double ury = (double?)annotElem.Attribute("ury") ?? 550;
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Create the appropriate annotation based on the type
                    Annotation annotation = CreateAnnotation(type, page, rect, annotElem);

                    // Set visibility flag
                    if (!isVisible)
                    {
                        // Hide the annotation from viewers
                        annotation.Flags = AnnotationFlags.Hidden;
                    }

                    // Add the annotation to the page
                    page.Annotations.Add(annotation);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"PDF saved with imported annotations: {outputPdf}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Factory method to create an annotation based on XFDF type.
    // Extend this method to support more annotation types as needed.
    private static Annotation CreateAnnotation(string type, Page page, Aspose.Pdf.Rectangle rect, XElement annotElem)
    {
        switch (type.ToLowerInvariant())
        {
            case "text":
                // Text annotation (sticky note)
                TextAnnotation textAnnot = new TextAnnotation(page, rect);
                textAnnot.Title = (string)annotElem.Attribute("title") ?? "Note";
                textAnnot.Contents = (string)annotElem.Attribute("contents") ?? string.Empty;
                textAnnot.Open = true;
                textAnnot.Icon = TextIcon.Note;
                return textAnnot;

            case "highlight":
                // Highlight annotation
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect);
                // Optional color (RGB values 0‑1)
                double r = (double?)annotElem.Attribute("r") ?? 1.0;
                double g = (double?)annotElem.Attribute("g") ?? 1.0;
                double b = (double?)annotElem.Attribute("b") ?? 0.0;
                highlight.Color = Aspose.Pdf.Color.FromRgb(r, g, b);
                highlight.Contents = (string)annotElem.Attribute("contents") ?? string.Empty;
                return highlight;

            // Add more cases (e.g., "underline", "strikeout", "ink") as required.

            default:
                // Fallback to a generic text annotation if type is unknown
                TextAnnotation fallback = new TextAnnotation(page, rect);
                fallback.Title = "Unsupported";
                fallback.Contents = $"Unsupported annotation type: {type}";
                fallback.Open = false;
                fallback.Icon = TextIcon.Note;
                return fallback;
        }
    }
}