"""
    Problem: Digit Tree Sum
    Link: https://app.codesignal.com/challenge/KhyzcMrKxRYhYqTm3
"""
def solution(t, p=''):
    d = solution
    l = t.left
    r = t.right
    v = str(t.value)
    s = int(v)
    if l: s+=d(l,p+v)
    if r: s+=d(r,p+v)
    return s - int(v) if l or r else int(p + str(s))