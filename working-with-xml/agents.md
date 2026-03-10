# AGENTS - working-with-xml

## Scope
- This folder contains examples for **working-with-xml**.
- Files are standalone `.cs` examples stored directly in this folder.

## Files in this folder
- [Apply-XSLT-transformation-to-convert-an-XML-file-into-a-PDF-document-taking-XML-as-input-and-produc](./Apply-XSLT-transformation-to-convert-an-XML-file-into-a-PDF-document-taking-XML-as-input-and-produc.cs)
- [Bind-XML-data-to-a-PDF-template-and-generate-a-PDF-document-accepting-XML-input-and-producing-PDF-o](./Bind-XML-data-to-a-PDF-template-and-generate-a-PDF-document-accepting-XML-input-and-producing-PDF-o.cs)
- [Create-a-PDF-document-from-an-XML-source-converting-XML-input-into-a-PDF-output-file](./Create-a-PDF-document-from-an-XML-source-converting-XML-input-into-a-PDF-output-file.cs)
- [Create-a-PDF-file-from-XML-data-using-the-defined-PDF-XML-schema-producing-PDF-output](./Create-a-PDF-file-from-XML-data-using-the-defined-PDF-XML-schema-producing-PDF-output.cs)
- [Create-a-PDF-file-from-XSL-FO-markup-by-processing-XSL-FO-input-and-producing-PDF-output](./Create-a-PDF-file-from-XSL-FO-markup-by-processing-XSL-FO-input-and-producing-PDF-output.cs)
- [Create-a-page-definition-by-providing-an-XML-configuration-that-specifies-page-dimensions-margins](./Create-a-page-definition-by-providing-an-XML-configuration-that-specifies-page-dimensions-margins.cs)
- [Generate-a-PDF-document-from-an-XML-source-applying-an-XSLT-stylesheet-using-XML-as-input-and-produ](./Generate-a-PDF-document-from-an-XML-source-applying-an-XSLT-stylesheet-using-XML-as-input-and-produ.cs)
- [Insert-a-Table-element-into-an-existing-XML-document-adhering-to-the-XML-schema](./Insert-a-Table-element-into-an-existing-XML-document-adhering-to-the-XML-schema.cs)
- [Insert-an-HtmlFragment-element-into-the-target-XML-file-adhering-to-correct-XML-formatting-conventi](./Insert-an-HtmlFragment-element-into-the-target-XML-file-adhering-to-correct-XML-formatting-conventi.cs)
- [Insert-graphic-elements-onto-a-page-by-supplying-an-XML-definition-that-specifies-their-properties](./Insert-graphic-elements-onto-a-page-by-supplying-an-XML-definition-that-specifies-their-properties.cs)
- [Modify-XML-nodes-at-runtime-by-programmatically-updating-content-while-maintaining-a-valid-XML-struc](./Modify-XML-nodes-at-runtime-by-programmatically-updating-content-while-maintaining-a-valid-XML-struc.cs)
- [Read-an-XML-file-from-the-given-location-and-parse-its-contents-according-to-the-XML-schema](./Read-an-XML-file-from-the-given-location-and-parse-its-contents-according-to-the-XML-schema.cs)
- [Specify-the-image-directory-when-converting-XML-to-PDF-using-XML-as-input-and-PDF-as-output](./Specify-the-image-directory-when-converting-XML-to-PDF-using-XML-as-input-and-PDF-as-output.cs)
- [Transform-XML-data-using-XSLT-to-generate-a-PDF-document-as-the-final-output](./Transform-XML-data-using-XSLT-to-generate-a-PDF-document-as-the-final-output.cs)
- [Transform-XSL-FO-markup-and-parameters-into-a-PDF-file-producing-PDF-output-from-XSL-FO-input](./Transform-XSL-FO-markup-and-parameters-into-a-PDF-file-producing-PDF-output-from-XSL-FO-input.cs)
- [Transform-the-XML-file-to-PDF-by-applying-an-XSL-stylesheet-that-generates-an-HTML-layout-before-ren](./Transform-the-XML-file-to-PDF-by-applying-an-XSL-stylesheet-that-generates-an-HTML-layout-before-ren.cs)


## Category Statistics
- Total examples: 16

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Annotations.XForm`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Pages`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.DocumentInfo`
- `Aspose.Pdf.FloatingBox`
- `Aspose.Pdf.Image`
- `Aspose.Pdf.JavaScript`
- `Aspose.Pdf.LogicalStructure.ParagraphElement`
- `Aspose.Pdf.LogicalStructure.ParagraphElement.SetText`
- `Aspose.Pdf.LogicalStructure.StructureElement`
- `Aspose.Pdf.LogicalStructure.StructureElement.AppendChild`
- `Aspose.Pdf.Page`
- `Aspose.Pdf.Page.Paragraphs.Add`
- `Aspose.Pdf.Pdf.Optimization.OptimizationOptions`

### Rules
- Instantiate a {doc} by loading an {input_pdf} file: new Document({input_pdf}).
- Obtain the metadata container {docInfo} via {doc}.Info and read its properties (Author, CreationDate, Keywords, ModDate, Subject, Title).
- Load a PDF document from a file path using the Document(string filePath) constructor: {input_pdf} → new Document({string_literal})
- Validate the loaded document against a specific PDF/A conformance level by calling Document.Validate(outputPath, PdfFormat.{pdfa_standard}) where {pdfa_standard} is a member of the PdfFormat enum (e.g., PDF_A_1A).
- Obtain the tagged content interface via {doc}.TaggedContent and set metadata using {doc}.TaggedContent.SetTitle({string_literal}) and {doc}.TaggedContent.SetLanguage({string_literal}) before saving.

### Warnings
- DocumentInfo properties may be null if the corresponding metadata is not present in the source PDF.
- The example creates an empty Document; real scenarios may need to add pages/content before saving.
- SetLanguage expects a BCP‑47 language tag (e.g., "en-US").
- Document.JavaScript is a dictionary‑like collection; changes are not persisted until Document.Save is called.
- The example casts Document.JavaScript.Keys to System.Collections.IList; ensure the cast is valid for the Aspose.PDF version used.

## General Tips
- See parent [agents.md](../agents.md) for repository-level patterns, conventions, and anti-patterns
- Review code examples in this folder for working-with-xml patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-10 | Run: `20260310_130343_23e77f`
<!-- AUTOGENERATED:END -->
