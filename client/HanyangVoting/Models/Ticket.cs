using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Ticket
    {
        /// <summary>
        /// 내부 식별 아이디
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 외부 식별 키
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 사용자 식별 이름
        /// </summary>
        public string Name { get; set; }

        public bool Commission { get; set; }

        public virtual Station Station { get; set; }
     }
}
