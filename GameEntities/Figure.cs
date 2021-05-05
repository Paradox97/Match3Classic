using System;

/*
public void BFS(int i, int j, int value, int MatchType, List<Match> matches)
{
    if (value == 11)
        return;

    int k = i;
    int z = j;
    bool isvisited;

    List<Match> Temp = new List<Match>();

    if (this.start_finding == 1)
    {
        this.start_finding = 0;

        if ((k + 1 < FIELD_SIZE) && (_field[k + 1, z] == value) )    //horizontal match - right 
        {
            Temp = matches;
            Temp.Add(new Match(k + 1, z, true));
            BFS(k + 1, z, value, 0, Temp);
        }

        if ((k - 1 > 0) && (_field[k - 1, z] == value))         //horizontal match - left
        {
            Temp = matches;
            Temp.Add(new Match(k - 1, z, true));
            BFS(k - 1, z, value, 0, Temp);
        }

        if ((z + 1 < FIELD_SIZE) && (_field[k, z + 1] == value))        //vertical match - down
        {
            Temp = matches;
            Temp.Add(new Match(k, z + 1, true));
            BFS(k, z + 1, value, 0, Temp);
        }

        if ((z - 1 > 0) && (_field[k, z - 1] == value))         //vertical match - up
        {
            Temp = matches;
            Temp.Add(new Match(k, z - 1, true));
            BFS(k, z - 1, value, 0, Temp);
        }
    }

    if (MatchType == 0)   //horizontal match
    {

        if ((k + 1 < FIELD_SIZE) && (_field[k + 1, z] == value) && (IsVisited(matches, k + 1, z) == 1))    //horizontal match - right  if not visited
        {

            Temp = matches;
            Temp.Add(new Match(k + 1, z, true));
            BFS(k + 1, z, value, 0, Temp);
        }

        if ((k - 1 > 0) && (_field[k - 1, z] == value) && (IsVisited(matches, k - 1, z) == 1))         //horizontal match - left
        {
            Temp = matches;
            Temp.Add(new Match(k - 1, z, true));
            BFS(k - 1, z, value, 0, Temp);
        }

        if ((z + 1 < FIELD_SIZE) && (_field[k, z + 1] == value) && (IsVisited(matches, k, z + 1) == 1))        //vertical match - down
        {
            Temp.Add(new Match(k, z, true));
            Temp.Add(new Match(k, z + 1, true));
            BFS(k, z + 1, value, 1, Temp);
        }

        if ((z - 1 > 0) && (_field[k, z - 1] == value) && (IsVisited(matches, k, z - 1) == 1))         //vertical match - up
        {
            Temp.Add(new Match(k, z, true));
            Temp.Add(new Match(k, z - 1, true));
            BFS(k, z - 1, value, 1, Temp);
        }

        if (Temp.Count >= MATCH_CONDITION)
        {
            for (int m = 0; m < Temp.Count; m++)
            {
                matches.Add(Temp[m]);
            }
            this._match = matches;
        }
    }


    if (MatchType == 1)     //vertical match
    {

        if ((k + 1 < FIELD_SIZE) && (_field[k + 1, z] == value) && (IsVisited(matches, k + 1, z) == 1))    //horizontal match - right 
        {
            Temp.Add(new Match(k, z, true));
            Temp.Add(new Match(k + 1, z, true));
            BFS(k + 1, z, value, 0, Temp);
        }

        if ((k - 1 > 0) && (_field[k - 1, z] == value) && (IsVisited(matches, k - 1, z) == 1))         //horizontal match - left
        {
            Temp.Add(new Match(k, z, true));
            Temp.Add(new Match(k - 1, z, true));
            BFS(k - 1, z, value, 0, Temp);
        }

        if ((z + 1 < FIELD_SIZE) && (_field[k, z + 1] == value) && (IsVisited(matches, k, z + 1) == 1))        //vertical match - down
        {
            Temp = matches;
            Temp.Add(new Match(k, z + 1, true));
            BFS(k, z + 1, value, 1, Temp);
        }

        if ((z - 1 > 0) && (_field[k, z - 1] == value) && (IsVisited(matches, k, z - 1) == 1))         //vertical match - up
        {
            Temp = matches;
            Temp.Add(new Match(k, z - 1, true));
            BFS(k, z - 1, value, 1, Temp);
        }

        if (Temp.Count >= MATCH_CONDITION)
        {
            for (int m = 0; m < Temp.Count; m++)
            {
                matches.Add(Temp[m]);
            }
            this._match = matches;
        }
    }
}
*/

/*

public int IsVisited(List<Match> matches, int i, int j)
{
    for (int m = 0; m < matches.Count; m++)
    {
        if ((matches[m].i == i) && (matches[m].j == j))
        {
            if (matches[m].isVisited == true)
                return 0;
            else
                return 1;
        }
    }
    return -1;
}*/


/*
 * 
 *         public void CheckMatch(int i, int j)
        {
            int[,] temp = new int[FIELD_SIZE, FIELD_SIZE];
            int value = this._field[i, j];

            if (value == 11)
                return;

            List<Match> hor_match = new List<Match>();
            List<Match> vert_match = new List<Match>();
            List<Match> all_matches = new List<Match>();

            if ((i + 2 < FIELD_SIZE) && (this._field[i + 1, j] == value) && (this._field[i + 2, j] == value))
            {
                hor_match.Add(new Match(i, j));
                hor_match.Add(new Match(i + 1, j));
                hor_match.Add(new Match(i + 2, j));
            }
        


            if (j + 2 < FIELD_SIZE)
            {
                if ((this._field[i, j + 1] == value) && (this._field[i, j + 2] == value))
                {
                    vert_match.Add(new Match(i, j));
                    vert_match.Add(new Match(i, j + 1));
                    vert_match.Add(new Match(i, j + 2));




                }

            }

        }

*/
public class Figure
{
	public Figure()
	{
	}
}
