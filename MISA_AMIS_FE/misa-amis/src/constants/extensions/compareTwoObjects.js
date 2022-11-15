export default function compareTwoObjects(object1, object2){
    // Nếu cả 2 object đều rỗng => true
    if(!object1 && !object2) return true;
    // Nếu chỉ 1 trong 2 object khác rỗng => false
    if((object1 && !object2) || (!object1 && object2)) return false;
    // Nếu cả 2 object đều ko rỗng, duyệt từng properti của từng object
    if(object1 && object2){
    for(let prop in object1){
        if(object1[prop] && !object2[prop]) return false;
        else if(object1[prop] && object2[prop]){
        if(object1[prop] !== object2[prop]) return false;
        }
    }
    for(let prop in object2){
        if(object2[prop] && !object1[prop]) return false;
        else if(object2[prop] && object1[prop]) {
        if(object1[prop] !== object2[prop]) return false;
        }
    }
    return true;
    }
}