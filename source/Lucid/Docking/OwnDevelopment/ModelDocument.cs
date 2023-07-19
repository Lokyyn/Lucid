namespace Lucid.Docking.OwnDevelopment;

public class ModelDocument<ModelType> : DarkDocument
{
    private ModelType _model;
    protected ModelType Model => _model;

    public ModelDocument(ModelType model)
    {
        this._model = model;
    }
}
