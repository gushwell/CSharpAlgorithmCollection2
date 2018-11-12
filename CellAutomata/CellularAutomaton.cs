using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CellAutomata
{
    class CellularAutomaton
    {

        private bool[] _cells;

        private Dictionary<string, bool> _rule;

        public CellularAutomaton(int rule, int width)
        {
            _cells = new bool[width];
            _cells[width / 2] = true;
            _rule = MakeRule(rule);
        }

        // ルール番号からルールを作成
        private Dictionary<string, bool> MakeRule(int rule)
        {
            var list = new[] {
                "111", "110", "101", "100", "011", "010", "001", "000"
            };
            return list.Reverse().Select(k => {
                var b = (rule & 0x01) == 0x01;
                rule = rule >> 1;
                return new { Key = k, Value = b };
            }).ToDictionary(p => p.Key, p => p.Value);
        }

        // すべての世代を列挙する。永遠に列挙するので注意。
        public IEnumerable<bool[]> AllGenerations()
        {
            yield return _cells;
            while (true)
            {
                List<bool> area = new List<bool>();
                foreach (var la in GetAdjoints())
                {
                    area.Add(_rule[LocalAreaToString(la)]);
                }
                _cells = area.ToArray();
                yield return _cells;
            }
        }

        // ３つのセルを文字列に変換
        string LocalAreaToString(bool[] area)
        {
            string s = "";
            foreach (var a in area)
            {
                s += a == true ? "1" : "0";
            }
            return s;
        }

        // 隣接した３つのセルを配列として順に取り出す。
        IEnumerable<bool[]> GetAdjoints()
        {
            bool[] area = new bool[3];
            for (int i = 0; i < _cells.Length; i++)
            {
                area[0] = (i == 0) ? false : _cells[i - 1];
                area[1] = _cells[i];
                area[2] = (i == _cells.Length - 1) ? false : _cells[i + 1];
                yield return area;
            }
        }
    }
}