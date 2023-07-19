namespace Lucid.Controls.DataClasses.Badge;

public class BadgeCollection
{
    private List<Badge> _badgesInner;
    internal BadgeColorCollection ColorCollection;

    public BadgeCollection(BadgeColorCollection badgeColorCollection = null)
    {
        _badgesInner = new List<Badge>();

        if (badgeColorCollection != null)
            ColorCollection = badgeColorCollection;
    }

    internal List<Badge> Badges => _badgesInner;

    public Badge this[string badgeId] => _badgesInner.FirstOrDefault(u => u.BadgeId == badgeId);
    public Badge this [int index] => _badgesInner[index];

    public Badge GetColor(string badgeId)
    {
        return _badgesInner.FirstOrDefault(u => u.BadgeId == badgeId);
    }

    /// <summary>
    /// Adds an Badge to the collection
    /// </summary>
    /// <param name="badgeId">The unique identifier of the badge</param>
    /// <param name="value">The text value</param>
    /// <param name="colorId">The color id for the visuals</param>
    /// <returns></returns>
    public Badge AddBadge(string badgeId, string value, string colorId)
    {
        var badge = new Badge(badgeId, value, colorId);
        _badgesInner.Add(badge); 
        TriggerEvent();

        return badge;
    }

    /// <summary>
    /// Adds the given badge to the collection
    /// </summary>
    /// <param name="badge"></param>
    public void AddBadge(Badge badge)
    {
        _badgesInner.Add(badge);
        TriggerEvent();
    }

    /// <summary>
    /// Updates the value for an badge with the given badgeId
    /// </summary>
    /// <param name="badgeId"></param>
    /// <param name="newValue"></param>
    public void UpdateBadgeValue(string badgeId, string newValue)
    {
        var badge = _badgesInner.FirstOrDefault(u => u.BadgeId == badgeId);

        if (badge != null)
        {
            badge.Value = newValue; 
            TriggerEvent();
        }
    }

    /// <summary>
    /// Removes an badge with the given badgeId
    /// </summary>
    public void RemoveBadge(string badgeId)
    {
        var badge = _badgesInner.FirstOrDefault(u => u.BadgeId == badgeId);

        if (badge != null)
        {
            _badgesInner.Remove(badge);
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        if (OnCollectionChanged != null)
            OnCollectionChanged();
    }

    #region Event - Region

    /// <summary>
    /// This event gets fired when the collection is changed
    /// </summary>
    public event BadgeCollectionChangedHandler OnCollectionChanged;

    public delegate void BadgeCollectionChangedHandler();

    #endregion
}
