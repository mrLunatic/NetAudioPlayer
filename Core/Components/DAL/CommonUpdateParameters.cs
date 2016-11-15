using System.Collections.Generic;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// ����� ��������� ���������� ��������
    /// </summary>
    public abstract class CommonUpdateParameters
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// �������������� �����
        /// </summary>
        public string Tag { get; set; }

        public virtual IDictionary<string, object> GetParams()
        {
            var items = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Name))
                items.Add(Item.NameField, Name);

            if (Rating.HasValue)
                items.Add(Item.RatingField, Rating.Value);

            if (!string.IsNullOrEmpty(Tag))
                items.Add(Item.TagField, Tag);

            return items;
        }

        
             

    }
}