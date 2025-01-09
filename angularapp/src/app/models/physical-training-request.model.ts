export interface PhysicalTrainingRequest{
    physicalTrainingRequestId?: number;
    userId:number;
    physicalTrainingId: number;
    requestDate:string;
    status:string;
    healthConditions:string;
    fitnessGoals:string;
    comments?:string;
}