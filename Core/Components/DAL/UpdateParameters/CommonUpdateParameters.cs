using System.Collections.Generic;
using Spartan.Common.Data;

namespace Spartan.ServerCore.Components.DAL.UpdateParameters
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

    }
}