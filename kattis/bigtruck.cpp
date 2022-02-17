/*
    Problem: Big Truck
    Link: https://open.kattis.com/problems/bigtruck
*/
#include <iostream>
#include <queue>
#include <vector>

using namespace std;

const int INF = 100001;

vector<int> min_costs, max_items;

struct Node
{
    int city;
    bool operator<(const Node& right) const {
        if (min_costs[city] == min_costs[right.city]) return max_items[right.city] > max_items[city];
        else return min_costs[city] > min_costs[right.city];
    }
};

struct Target
{
    int city;
    int cost;
};

int main() {

    int city_nr;
    cin >> city_nr;

    vector<int> items(city_nr);
    vector<vector<Target>> map(city_nr);
    vector<bool> passed(city_nr);

    for (int idx = 0; idx < city_nr; idx++) {
        min_costs.push_back(INF);
        max_items.push_back(0);
        cin >> items[idx];
    }

    int edgeNr;
    cin >> edgeNr;

    for (int idx = 0; idx < edgeNr; idx++) {
        int u, v, w;
        cin >> u >> v >> w;
        u--; v--;
        map[u].push_back({ v, w });
        map[v].push_back({ u, w });
    }

    min_costs[0] = 0;
    max_items[0] = items[0];

    priority_queue<Node> queue;
    queue.push({ 0 });

    while (!queue.empty()) {
        Node node = queue.top();
        queue.pop();
        if (passed[node.city]) continue;
        for (Target& next : map[node.city]) {
            int cost = min_costs[node.city] + next.cost;
            int item = max_items[node.city] + items[next.city];
            if (min_costs[next.city] > cost) {
                min_costs[next.city] = cost;
                max_items[next.city] = item;
            } else if (min_costs[next.city] == cost && max_items[next.city] < item) {
                max_items[next.city] = item;
            } else continue;
            queue.push({ next.city });
        }
        passed[node.city] = true;
    }
    if (min_costs[city_nr - 1] >= INF) cout << "impossible" << endl;
    else cout << min_costs[city_nr - 1] << ' ' << max_items[city_nr - 1] << endl;

}