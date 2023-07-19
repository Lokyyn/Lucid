namespace Lucid.Docking;

public class LucidModelDocument<ModelType> : LucidDocument
{
    private ModelType _model;
    protected ModelType Model => _model;

    public LucidModelDocument(ModelType model)
    {
        _model = model;
    }
}
