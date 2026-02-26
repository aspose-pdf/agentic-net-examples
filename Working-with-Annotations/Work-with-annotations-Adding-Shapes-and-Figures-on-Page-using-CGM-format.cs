using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string cgmPath      = "input.cgm";          // CGM input (input‑only format)
        const string imagePath    = "rubber.jpg";         // Image for image stamp
        const string outputPdf    = "annotated_output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the CGM file (CGM is input‑only, we save as PDF)
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            using (Document doc = new Document(cgmPath, loadOptions))
            {
                // -------------------------------------------------
                // 1. Add a rubber‑stamp annotation (icon based)
                // -------------------------------------------------
                Aspose.Pdf.Rectangle stampRect1 = new Aspose.Pdf.Rectangle(100, 500, 200, 560);
                StampAnnotation rubberStamp = new StampAnnotation(doc.Pages[1], stampRect1);
                rubberStamp.Icon = StampIcon.NotForPublicRelease; // built‑in stamp icon
                rubberStamp.Color = Aspose.Pdf.Color.LightGray;
                doc.Pages[1].Annotations.Add(rubberStamp);

                // -------------------------------------------------
                // 2. Add an image‑stamp annotation (custom image)
                // -------------------------------------------------
                Aspose.Pdf.Rectangle stampRect2 = new Aspose.Pdf.Rectangle(220, 500, 320, 560);
                StampAnnotation imageStamp = new StampAnnotation(doc.Pages[1], stampRect2);
                // Load image stream and assign to the annotation
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    imageStamp.Image = imgStream;
                }
                imageStamp.Color = Aspose.Pdf.Color.Transparent; // keep original colors
                doc.Pages[1].Annotations.Add(imageStamp);

                // -------------------------------------------------
                // 3. Add an Ink annotation (free‑hand shape)
                // -------------------------------------------------
                // Define a simple triangle path
                IList<Aspose.Pdf.Point[]> inkList = new List<Aspose.Pdf.Point[]>();
                inkList.Add(new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(100, 100),
                    new Aspose.Pdf.Point(200, 200),
                    new Aspose.Pdf.Point(300, 100),
                    new Aspose.Pdf.Point(100, 100) // close the shape
                });

                Aspose.Pdf.Rectangle inkRect = new Aspose.Pdf.Rectangle(50, 50, 350, 250);
                InkAnnotation inkAnno = new InkAnnotation(doc.Pages[1], inkRect, inkList);
                inkAnno.Color = Aspose.Pdf.Color.Blue;
                inkAnno.Opacity = 0.7;
                inkAnno.Border = new Border(inkAnno) { Width = 2 };
                doc.Pages[1].Annotations.Add(inkAnno);

                // -------------------------------------------------
                // 4. Add a Figure element to the logical structure
                // -------------------------------------------------
                ITaggedContent tagged = doc.TaggedContent;
                // Set language and title for accessibility (optional)
                tagged.SetLanguage("en-US");
                tagged.SetTitle("CGM with Annotations");

                StructureElement root = tagged.RootElement;

                // Create a Figure element that represents a graphic object
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample figure added via logical structure.";
                // Append the figure to the root of the structure tree
                root.AppendChild(figure);

                // -------------------------------------------------
                // Save the resulting PDF
                // -------------------------------------------------
                doc.Save(outputPdf);
                Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}